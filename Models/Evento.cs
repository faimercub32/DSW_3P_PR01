namespace DSW_3P_PR01.Models;

public class Evento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Categoria { get; set; } = "";
    public DateTimeOffset Fecha { get; set; }
    public string Lugar { get; set; } = "";
    public int CupoMaximo { get; set; }
    public bool Activo { get; set; }
}
