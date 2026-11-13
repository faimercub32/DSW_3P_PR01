using DSW_3P_PR01.Models;

namespace DSW_3P_PR01.Services;

public interface IEventoService
{
    List<Evento> ObtenerTodas();
    Evento? ObtenerPorId(int id);
    Evento Agregar(Evento nuevaEvento);
    bool Actualizar(int id, Evento actualizado);
    bool Eliminar(int id);
}