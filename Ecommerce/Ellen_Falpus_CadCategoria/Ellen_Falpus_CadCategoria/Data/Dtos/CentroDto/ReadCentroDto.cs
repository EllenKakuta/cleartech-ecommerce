using Ellen_Falpus_CadCategoria.Modelos;
using System;

namespace Ellen_Falpus_CadCategoria.Data.Dtos.CentroDto
{
    public class ReadCentroDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public Produto Produto { get; set; }  
    }
}
