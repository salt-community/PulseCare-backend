using PulseCare.API.Data.Entities.Users;

public interface IUserRepository
{
    Task AddAdminAsync(Doctor newAdmin);
    Task AddUserAsync(User user);
    Task<Patient?> GetPatientFromUserAsync(Guid userId);
    Task<User?> GetUserAsync(string clerkId);
    Task RemovePatientAsync(Patient patient);
    Task<bool> IsExistingPatientAsync(string userId);
    Task<bool> IsExistingAdminAsync(Guid userId);
    Task AddPatientAsync(Patient newPatient);
}