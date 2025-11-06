using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;

namespace ProfServer.Application.Services
{
    public class MachineStatusService : IMachineStatusService
    {
        private readonly IMachineStatusRepository _machineStatusRepository;
        private readonly ILogger<MachineStatusService> _logger;

        public MachineStatusService(
            IMachineStatusRepository machineStatusRepository,
            ILogger<MachineStatusService> logger)
        {
            _machineStatusRepository = machineStatusRepository;
            _logger = logger;
        }

        public Task<MachineStatus> CreateMachineStatusAsync(CreateMachineStatusRequest request)
        {
            try
            {
                MachineStatus machineStatus = new() 
                {
                    Name = request.Name
                }; 

                var statusId = _machineStatusRepository.AddMachineStatusAsync(machineStatus);

                return GetMachineStatusByIdAsync(statusId.Result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating machine status");
                throw;
            }
        }

        public async Task<bool> DeleteMachineStatusAsync(int id)
        {
            try
            {
                await GetMachineStatusByIdAsync(id);

                return await _machineStatusRepository.DeleteMachineStatusAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting machine status");
                throw;
            }
        }

        public async Task<MachineStatus> GetMachineStatusByIdAsync(int id)
        {
            try
            {
                var status = await _machineStatusRepository.GetMachineStatusByIdAsync(id);
                if (status == null)
                    throw new NotFoundException(nameof(MachineStatus), id);

                return status;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving machine status");
                throw;
            }
        }

        public async Task<IEnumerable<MachineStatus>> GetMachineStatusesAsync()
        {
            try
            {
                return await _machineStatusRepository.GetMachineStatusesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving machine statuses");
                throw;
            }
        }

        public async Task<MachineStatus> UpdateMachineStatusAsync(UpdateMachineStatusRequest request)
        {
            try
            {
                await GetMachineStatusByIdAsync(request.Id);

                var machineStatus = new MachineStatus
                {
                    Id = request.Id,
                    Name = request.Name
                };

                if (!await _machineStatusRepository.UpdateMachineStatusAsync(machineStatus))
                    throw new ConflictException("Failed to update machine status");

                return await GetMachineStatusByIdAsync(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating machine status");
                throw;
            }
        }
    }
}
