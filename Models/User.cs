namespace Backend_Prueba_Tecnica.Models
{
    // Equivalente a usar @Entity en Spring Boot
    public class User
    {
        // En EF Core, una propiedad llamada "Id" se toma automáticamente como Primary Key, equivalente a @Id
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;
    }
}
