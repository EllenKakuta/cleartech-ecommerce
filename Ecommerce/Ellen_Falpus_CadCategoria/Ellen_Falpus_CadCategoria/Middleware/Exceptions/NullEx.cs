using System;

namespace Ellen_Falpus_CadCategoria.Middleware.Exceptions
{
    public class NullEx : Exception
    {
        private const string mensagemPadrao = "Favor verificar dados inseridos";

        public NullEx()
            : base(mensagemPadrao)
        { }

        public NullEx(string mensagem)
            : base(mensagem)
        { }

        public NullEx(Exception innerException)
            : base(mensagemPadrao, innerException)
        { }
    }
}

