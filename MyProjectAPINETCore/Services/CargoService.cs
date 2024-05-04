using Microsoft.EntityFrameworkCore;
using MyProjectAPINETCore.Models;

namespace MyProjectAPINETCore.Services
{
    public class CargoService : ICargoService
    {
        private readonly DatabaseExampleContext _context;
        public CargoService(DatabaseExampleContext context)
        {
            _context = context;
        }

        public async Task<Cargo> obtenerCargo(int cargoid)
        {
            return await _context.Cargos.Where(c => c.CargoId == cargoid).FirstOrDefaultAsync();
        }

        public async Task<List<Cargo>> obtenerCargos()
        {
            return await _context.Cargos.ToListAsync();
        }

        public async Task<List<Usuario>> obtenerUsuariosPorCargo(int cargoid)
        {
            return await _context.Usuarios.Where(u=>u.IdCargo==cargoid).Include(u=>u.oCargo).ToListAsync();
        }

        public async Task<bool> registrarCargo(Cargo cargo)
        {
            _context.Cargos.Add(cargo);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
