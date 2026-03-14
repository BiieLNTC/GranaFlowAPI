using System.ComponentModel;

namespace GranaFlow.Domain.Enums
{
    public enum ETipoTransacao
    {
        [Description("Despesa")]
        Despesa = 1,

        [Description("Receita")]
        Receita = 2,
    }
}

