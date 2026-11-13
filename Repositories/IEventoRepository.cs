using DSW_3P_PR01.Models;
namespace DSW_3P_PR01.Repositories;

public interface IEventoRepository
{
    List<Evento> ObtenerTodos();
    Evento? ObtenerPorId(int id);
    Evento Agregar(Evento nuevoEvento);
    bool Actualizar(int id, Evento evento);
    bool Eliminar(int id);
}