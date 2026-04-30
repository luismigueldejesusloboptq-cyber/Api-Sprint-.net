
using Api_Lanchonete_Sprint;
using Api_Lanchonete_Sprint.DTOs.Request;
using Api_Lanchonete_Sprint.DTOs.Response;

namespace Api_Lanchonete_Sprint.Services.Interfaces
{
    public interface IFornecedorService
    {
        Task<List<FornecedorResponseDTO>> GetAll();
        Task<List<FornecedorRequestDTO>> GetById(int id);
        Task<FornecedorResponseDTO> Create
        (
         FornecedorRequestDTO dto
        );

        Task<FornecedorResponseDTO> Update
        (
            int id, FornecedorRequestDTO dto
        );

        Task<bool> Delete ( int id ); 

    }
}
