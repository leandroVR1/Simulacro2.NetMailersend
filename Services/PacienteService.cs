// Definición de la clase PacienteService en el espacio de nombres Simulacro2.Services.
namespace Simulacro2.Services
{
    // La clase PacienteService implementa la interfaz IPacienteService, que define los métodos necesarios para interactuar con la lógica de negocios relacionada con los pacientes.
    public class PacienteService : IPacienteService
    {
        // Campo privado para acceder a la base de datos.
        private readonly BaseContext _context;

        // Constructor de la clase PacienteService que inicializa el campo _context.
        public PacienteService(BaseContext context)
        {
            _context = context;
        }

        // Método para crear un nuevo paciente.
        public async Task<Paciente> CreatePaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        // Método para eliminar un paciente.
        public async Task<Paciente> DeletePaciente(int Id)
        {
            var paciente = await _context.Pacientes.FindAsync(Id);
            if (paciente == null)
            {
                return null;
            }

            paciente.Estado = EstadoEnum.Eliminado;
            await _context.SaveChangesAsync();
            return paciente;
        }

        // Método para obtener todos los pacientes disponibles.
        public async Task<IEnumerable<Paciente>> GetAllPacientes()
        {
            return await _context.Pacientes.Where(m => m.Estado == EstadoEnum.Disponible).ToListAsync();
        }

        // Método para obtener un paciente por su ID.
        public async Task<Paciente> GetPacienteById(int Id)
        {
            return await _context.Pacientes.FindAsync(Id);
        }

        // Este método actualiza la información de un paciente en la base de datos.
public async Task<Paciente> UpdatePaciente(int Id, Paciente paciente)
{
    // Busca al paciente existente en la base de datos utilizando el Id proporcionado.
    var existingPaciente = await _context.Pacientes.FindAsync(paciente.Id);

    // Si no se encuentra ningún paciente, devuelve null.
    if (existingPaciente == null)
    {
        return null;
    }

    // Actualiza las propiedades del paciente con los nuevos valores proporcionados.
    existingPaciente.Nombre = paciente.Nombre;
    existingPaciente.Apellido = paciente.Apellido;
    existingPaciente.Telefono = paciente.Telefono;
    existingPaciente.Direccion = paciente.Direccion;

    // Guarda los cambios en la base de datos.
    await _context.SaveChangesAsync();

    // Devuelve el objeto Paciente actualizado.
    return existingPaciente;
}

        // Método para obtener todos los pacientes eliminados.
        public async Task<IEnumerable<Paciente>> GetDeletedPaciente()
        {
            return await _context.Pacientes.Where(m => m.Estado == EstadoEnum.Eliminado).ToListAsync();
        }

        // Método para obtener el número de citas de un paciente específico.
        public async Task<int> GetCitasCountByPacienteId(int pacienteId)
        {
            var paciente = await _context.Pacientes.Include(p => p.Citas).FirstOrDefaultAsync(p => p.Id == pacienteId);
            if (paciente!= null)
            {
                return paciente.Citas.Count;
            }
            return 0;
        }

        // Método para obtener el historial médico de un paciente específico.
        public async Task<IEnumerable<Cita>> GetHistorialMedico(int pacienteId)
        {
            return await _context.Citas
               .Where(c => c.PacienteId == pacienteId && c.Estado == EstadoEnum.Disponible)
               .ToListAsync();
        }
    }
}