using System;

namespace BoletoNetCore.Banco
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class CarteiraCodigoAttribute : Attribute
    {
        internal CarteiraCodigoAttribute(params string[] codigo)
        {
            Codigos = codigo;
        }

        internal string[] Codigos { get; }
    }
}