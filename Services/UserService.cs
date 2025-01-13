using Microsoft.EntityFrameworkCore;
using TicketSystem.Utilities;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Services;

public class UserService
{
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    public async Task<bool> RegisterUserAsync(string username, string email, string password)
    {
        try
        {
            var existingUser = await _context.Users
                    .FirstOrDefaultAsync(x => x.Username == username || x.Email == email);
            if (existingUser != null)
            {
                // User already exists with the same username or email
                return false;
            }
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordSalt = Hashing.CreateSalt(32),
                isAdmin = false
            };

            user.PasswordHash = Hashing.CreatePasswordHash(password, user.PasswordSalt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<User?> LoginAsync(string username, string password)
    {
        try
        {
            var FoundUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (FoundUser == null || FoundUser.PasswordHash != Hashing.CreatePasswordHash(password, FoundUser.PasswordSalt))
            {
                return null;
            }

            return FoundUser;
        }
        catch
        {
            throw new NotImplementedException();
        }
    }
    public async Task<User?> GetAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Tickets) // Include tickets
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .Include(u => u.Tickets) // Include orders placed by users
            .ToListAsync();
    }

    public async Task<bool> DeleteAsync(int userId, int loggedInUserId)
    {
        try
        {
            var userToDelete = await _context.Users.FindAsync(userId);
            var loggedInUser = await _context.Users.FindAsync(loggedInUserId);

            // Only admins can delete users, and they can't delete themselves
            if (userToDelete == null || loggedInUser == null || !loggedInUser.isAdmin || loggedInUser.Id == userId)
            {
                return false;
            }

            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> UpdateAsync(User updatedUser, int loggedInUserId)
    {
        try
        {
            var userToUpdate = await _context.Users.FindAsync(updatedUser.Id);
            var loggedInUser = await _context.Users.FindAsync(loggedInUserId);

            // Only admins can update other users, and they cannot update their own role or username
            if (userToUpdate == null || loggedInUser == null || !loggedInUser.isAdmin || loggedInUser.Id == updatedUser.Id)
            {
                return false;
            }

            // Update fields as necessary
            userToUpdate.Email = updatedUser.Email ?? userToUpdate.Email;
            userToUpdate.PasswordHash = updatedUser.PasswordHash ?? userToUpdate.PasswordHash;
            userToUpdate.PasswordSalt = updatedUser.PasswordSalt ?? userToUpdate.PasswordSalt;

            _context.Users.Update(userToUpdate);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<int> GetTotalUsersAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<bool> IsUserAdmin(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.isAdmin ?? false;
    }

    public async Task<bool> DoesUserExist(string username, string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username || x.Email == email) != null;
    }
}