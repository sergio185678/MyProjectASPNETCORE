using MyProjectAPINETCore.Models;

namespace MyProjectAPINETCore.Services
{
    public interface ICargoService
    {
        Task<List<Cargo>> obtenerCargos();
        Task<List<Usuario>> obtenerUsuariosPorCargo(int cargoid);
        Task<Cargo> obtenerCargo(int cargoid);
        Task<bool> registrarCargo(Cargo cargo);
    }
}
