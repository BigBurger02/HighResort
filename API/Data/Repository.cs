using Microsoft.EntityFrameworkCore;

using API.Interfaces;
using API.Models;

namespace API.Data;

public class Repository : IRepository
{
    private readonly APIContext _context;

    public Repository(APIContext context)
    {
        _context = context;
    }

    #region Rooms
    
    public IEnumerable<Room> GetAllRooms()
    {
        return _context.Room
            .AsNoTracking()
            .AsEnumerable();
    }

    public async Task<Room?> GetRoomAsync(int id)
    {
        var room = await _context.Room
            .FindAsync(id);

        return room;
    }

    public async void UpdateRoom(Room room)
    {
        _context.Entry(room).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if ((_context.Room?.Any(e => e.Id == room.Id)).GetValueOrDefault() == false)
            {
                throw new KeyNotFoundException("Item not found in the DataBase", ex);
            }
            else
            {
                throw ex;
            }
        }
    }

    public async void CreateRoom(Room room)
    {
        _context.Room.Add(room);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRoom(int id)
    {
        var room = await GetRoomAsync(id);
        if (room == null)
        {
            return false;
        }
        else
        {
            _context.Room.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
    }
    
    #endregion
}