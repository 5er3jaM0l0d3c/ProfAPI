using AutoMapper;
using ProfServer.Application.DTOs;
using ProfServer.Application.DTOs.Requests;
using ProfServer.Application.Interfaces;
using ProfServer.Models;
using ProfServer.Models.Official;
using System.Linq;
using System.Reflection.PortableExecutable;

namespace ProfServer.Application.Services
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IMaintenanceRepository _maintenanceRepository;
        private readonly IUserService _userService;
        private readonly IMachineService _machineService;
        private readonly IProblemService _problemService;
        private readonly IWorkDescriptionService _workDescriptionService;
        private readonly IMaintenance_ProblemService _maintenance_ProblemService;
        private readonly IMaintenance_WorkDescriptionService _maintanence_WorkDescriptionService;
        private readonly IMapper _mapper;
        private readonly ILogger<MaintenanceService> _logger;

        public MaintenanceService(IMaintenanceRepository maintenanceRepository, IUserService userService, IMachineService machineService, IProblemService problemService, IWorkDescriptionService workDescriptionService, IMaintenance_ProblemService maintenance_ProblemService, IMaintenance_WorkDescriptionService maintenance_WorkDescriptionService, IMapper mapper, ILogger<MaintenanceService> logger)
        {
            _maintenanceRepository = maintenanceRepository;
            _userService = userService;
            _machineService = machineService;
            _problemService = problemService;
            _workDescriptionService = workDescriptionService;
            _maintenance_ProblemService = maintenance_ProblemService;
            _maintanence_WorkDescriptionService = maintenance_WorkDescriptionService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<MaintenanceDTO> CreateMaintenance(CreateMaintenanceRequest request)
        {
            try
            {
                Maintenance maintenance = _mapper.Map<Maintenance>(request);

                var maintenanceId = await _maintenanceRepository.CreateMaintanance(maintenance);

                var existingMaintenance = await _maintenanceRepository.GetMaintenanceById(maintenanceId);

                return _mapper.Map<MaintenanceDTO>(existingMaintenance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating maintenance for machine {MachineId}", request.MachineId);
                throw;
            }
        }

        public async Task<bool> DeleteMaintenance(int id)
        {
            try
            {
                await _maintenanceRepository.GetMaintenanceById(id);
                return await _maintenanceRepository.DeleteMaintenance(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting maintenance with id {MaintenanceId}", id);
                throw;
            }
        }

        public async Task<MaintenanceDTO> GetMaintenanceById(int id)
        {
            try
            {
                var maintenance = await _maintenanceRepository.GetMaintenanceById(id);
                if (maintenance == null)
                    throw new NotFoundException(nameof(Maintenance), id);

                maintenance.User = await _userService.GetUserByIdAsync(maintenance.UserId);
                maintenance.Machine = await _machineService.GetMachineByIdAsync(maintenance.MachineId);

                var workDescriptions = (await _maintanence_WorkDescriptionService.GetAllAsync()).Where(x => x.Maintenance.Id == id);
                var problemsIds = (await _maintenance_ProblemService.GetAllProblemsAsync()).Where(x => x.Maintenance.Id == id);
                var allProblems = await _problemService.GetAllProblems();
                var allWorkDescriptions = await _workDescriptionService.GetAllWorkDescriptions();

                maintenance.ProblemsIds = problemsIds.Select(p => p.Problem.Id).ToList();
                maintenance.WorkDescriptionsIds = workDescriptions.Select(w => w.WorkDescribtion.Id).ToList();

                maintenance.Problems = allProblems.Where(p => maintenance.ProblemsIds.Contains(p.Id));

                maintenance.WorkDescriptiones = allWorkDescriptions.Where(w => maintenance.WorkDescriptionsIds.Contains(w.Id));

                return _mapper.Map<MaintenanceDTO>(maintenance);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting maintenance with id {MaintenanceId}", id);
                throw;
            }
        }

        public async Task<IEnumerable<MaintenanceDTO>> GetMaintenancesByMachineId(int machineId)
        {
            try
            {
                var maintenances = await _maintenanceRepository.GetMaintenancesByMachineId(machineId);
                var problems = await _problemService.GetAllProblems();
                var workDescriptions = await _workDescriptionService.GetAllWorkDescriptions();
                var problemsIds = (await _maintenance_ProblemService.GetAllProblemsAsync()).Where(x => x.MaintenanceId == machineId);
                var workDescriptionsIds = (await _maintanence_WorkDescriptionService.GetAllAsync()).Where(x => x.MaintenanceId == machineId);

                foreach (var maintenance in maintenances)
                {
                    maintenance.User = await _userService.GetUserByIdAsync(maintenance.UserId);
                    maintenance.Machine = await _machineService.GetMachineByIdAsync(maintenance.MachineId);
                    maintenance.ProblemsIds = problemsIds.Select(p => p.ProblemId);
                    maintenance.WorkDescriptionsIds = workDescriptionsIds.Select(w => w.WorkDescribtionId);

                    maintenance.Problems = problems.Where(p => maintenance.ProblemsIds.Contains(p.Id));
                    maintenance.WorkDescriptiones =  workDescriptions.Where(w => maintenance.WorkDescriptionsIds.Contains(w.Id));
                }

                return _mapper.Map<IEnumerable<MaintenanceDTO>>(maintenances);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting maintenances for machine {MachineId}", machineId);
                throw;
            }
        }

        public async Task<IEnumerable<MaintenanceDTO>> GetMaintenancesByUserId(int userId)
        {
            try
            {
                var maintenances = await _maintenanceRepository.GetMaintenancesByUserId(userId);
                var problems = await _problemService.GetAllProblems();
                var workDescriptions = await _workDescriptionService.GetAllWorkDescriptions();
                var problemsIds = await _maintenance_ProblemService.GetAllProblemsAsync();
                var workDescriptionsIds = await _maintanence_WorkDescriptionService.GetAllAsync();

                foreach (var maintenance in maintenances)
                {
                    maintenance.User = await _userService.GetUserByIdAsync(maintenance.UserId);
                    maintenance.Machine = await _machineService.GetMachineByIdAsync(maintenance.MachineId);
                    maintenance.ProblemsIds = problemsIds.Select(p => p.ProblemId);
                    maintenance.WorkDescriptionsIds = workDescriptionsIds.Select(p => p.WorkDescribtionId);


                    maintenance.Problems = problems.Where(p => maintenance.ProblemsIds.Contains(p.Id));
                    maintenance.WorkDescriptiones = workDescriptions.Where(w => maintenance.WorkDescriptionsIds.Contains(w.Id));
                }
                return _mapper.Map<IEnumerable<MaintenanceDTO>>(maintenances);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting maintenances for user {UserId}", userId);
                throw;
            }
        }

        public async Task<MaintenanceDTO> UpdateMaintenance(UpdateMaintenanceRequest request)
        {
            try
            {
                await GetMaintenanceById(request.Id);

                Maintenance maintenance = _mapper.Map<Maintenance>(request);

                if (!await _maintenanceRepository.UpdateMaintenance(maintenance))
                    throw new ConflictException("Failed to update maintenance.");

                return await GetMaintenanceById(request.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating maintenance with id {MaintenanceId}", request.Id);
                throw;
            }
        }
    }
}
