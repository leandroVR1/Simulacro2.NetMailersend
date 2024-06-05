// Definición de la interfaz IMedicoService en el espacio de nombres Simulacro2.Interfaces.
namespace Simulacro2.Interfaces
{
    // La interfaz IMedicoService define los métodos que se pueden utilizar para interactuar con la lógica de negocios relacionada con los médicos.
    public interface IMedicoService
    {
        // Método para obtener todos los médicos disponibles.
        Task<IEnumerable<Medico>> GetAllMedicos();

        // Método para obtener un médico por su ID.
        Task<Medico> GetMedicoById(int Id);

        // Método para crear un nuevo médico.
        Task<Medico> CreateMedico(Medico medico);

        // Método para actualizar un médico existente.
        Task<Medico> UpdateMedico(int Id, Medico medico);

        // Método para eliminar un médico.
        Task<Medico> DeleteMedico(int Id);

        // Método para obtener todos los médicos eliminados.
        Task<IEnumerable<Medico>> GetDeletedMedico();

        // Método para obtener todos los pacientes asociados a un médico específico.
        Task<IEnumerable<Paciente>> GetPacientesDeMedico(int medicoId);
    }
}