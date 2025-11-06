using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Application.Mappings;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Reflection.Metadata;

namespace ProfServer.Application.Services
{
    public class MachineService : IMachineService
    {
        private readonly IMachineRepository _machineRepository;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IMachineStatusService _machineStatusService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IMapper _mapper;
        private readonly ILogger<MachineService> _logger;

        public MachineService(IMachineRepository machineRepository,
            IPaymentTypeService paymentTypeService, 
            IMachineStatusService machineStatusService,
            IManufacturerService manufacturerService,
            IMapper mapper,
            ILogger<MachineService> logger)
        {
            _machineRepository = machineRepository;
            _paymentTypeService = paymentTypeService;
            _machineStatusService = machineStatusService;
            _manufacturerService = manufacturerService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MachineDTO> CreateMachineAsync(CreateMachineRequest machine)
        {
            try
            {
                Machine machineEntity = _mapper.Map<Machine>(machine);

                var machineId = await _machineRepository.AddMachineAsync(machineEntity);

                return await GetMachineByIdAsync(machineId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating machine");
                throw;
            }

        }

        public async Task<bool> DeleteMachineAsync(int id)
        {
            try
            {
                await GetMachineByIdAsync(id); // Verify existence

                return await _machineRepository.DeleteMachineAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting machine");
                throw;
            }
        }

        public async Task<MachineDTO> GetMachineByIdAsync(int id)
        {
            try
            {
                var machine = await _machineRepository.GetMachineByIdAsync(id);
                if (machine == null)
                {
                    _logger.LogError("Machine with id {MachineId} not found for deletion", id);
                    throw new NotFoundException(nameof(Machine), id);
                }

                machine.PaymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(machine.PaymentTypeId);
                machine.Status = await _machineStatusService.GetMachineStatusByIdAsync(machine.StatusId);
                machine.Manufacturer = await _manufacturerService.GetManufacturerByIdAsync(machine.ManufacturerId);

                return _mapper.Map<MachineDTO>(machine);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving machine with id {MachineId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<MachineDTO>> GetMachinesAsync()
        {
            try
            {
                var machines = await _machineRepository.GetMachinesAsync();

                foreach (var machine in machines)
                {
                    machine.PaymentType = await _paymentTypeService.GetPaymentTypeByIdAsync(machine.PaymentTypeId);
                    machine.Status = await _machineStatusService.GetMachineStatusByIdAsync(machine.StatusId);
                    machine.Manufacturer = await _manufacturerService.GetManufacturerByIdAsync(machine.ManufacturerId);
                }

                return _mapper.Map<IEnumerable<MachineDTO>>(machines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving machines");
                throw;
            }
        }

        public async Task<MachineDTO> UpdateMachineAsync(UpdateMachineRequest machine)
        {
            try
            {
                await GetMachineByIdAsync(machine.Id); // Verify existence

                var machineEntity = _mapper.Map<Machine>(machine);
                if(!await _machineRepository.UpdateMachineAsync(machineEntity))
                    throw new ConflictException($"Machine with Id {machine.Id} could not be updated");
                
                return await GetMachineByIdAsync(machineEntity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating machine with id {MachineId}", machine.Id);
                throw;
            }
        }
    }
}
