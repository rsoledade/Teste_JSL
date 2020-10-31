using Newtonsoft.Json;

namespace TesteJSL.Domain.Entities
{
    public class Veiculo : Base
    {
        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Placa { get; set; }

        public int Eixos { get; set; }

        public int IdMotorista { get; set; }

        [JsonIgnore]
        public virtual Motorista Motorista { get; set; }
    }
}
