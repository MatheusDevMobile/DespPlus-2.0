using System.Linq;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public static class ValidacaoUtils
    {
        public static bool ValidarCPF(string valor)
        {
            if (valor == null)
                return false;

            valor = valor.Replace(".", "").Replace("-", "");
            if (valor.Length != 11)
                return false;

            //Tira cpf's teste
            if (valor == "00000000000" || valor == "11111111111" || valor == "22222222222" || valor == "33333333333" || valor == "44444444444" ||
                valor == "55555555555" || valor == "66666666666" || valor == "77777777777" || valor == "88888888888" || valor == "99999999999")
                return false;

            // Valida primeiro dígito verificador
            var soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(valor.ElementAt(i).ToString()) * (10 - i);
            }
            var resto = soma * 10 % 11 % 10;

            if (resto != int.Parse(valor.ElementAt(9).ToString()))
                return false;

            // Valida segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(valor.ElementAt(i).ToString()) * (11 - i);
            }
            resto = soma * 10 % 11 % 10;

            if (resto != int.Parse(valor.ElementAt(10).ToString()))
                return false;

            return true;
        }
        public static bool ValidarCNPJ(string valor)
        {
            if (valor == null)
                return false;

            valor = valor.Replace(".", "").Replace("-", "").Replace("/", "");
            if (valor.Length != 14)
                return false;

            // Elimina CNPJs invalidos conhecidos
            if (valor == "00000000000000" ||
                valor == "11111111111111" ||
                valor == "22222222222222" ||
                valor == "33333333333333" ||
                valor == "44444444444444" ||
                valor == "55555555555555" ||
                valor == "66666666666666" ||
                valor == "77777777777777" ||
                valor == "88888888888888" ||
                valor == "99999999999999")
                return false;

            // Valida DVs
            var tamanho = valor.Length - 2;
            var numeros = valor.Substring(0, tamanho);
            var digitos = valor.Substring(tamanho);
            var soma = 0;
            var pos = tamanho - 7;
            for (int i = tamanho; i >= 1; i--)
            {
                soma += int.Parse(numeros.ElementAt(tamanho - i).ToString()) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            var resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != int.Parse(digitos.ElementAt(0).ToString()))
                return false;

            tamanho = tamanho + 1;
            numeros = valor.Substring(0, tamanho);
            soma = 0;
            pos = tamanho - 7;
            for (int i = tamanho; i >= 1; i--)
            {
                soma += int.Parse(numeros.ElementAt(tamanho - i).ToString()) * pos--;
                if (pos < 2)
                    pos = 9;
            }
            resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
            if (resultado != int.Parse(digitos.ElementAt(1).ToString()))
                return false;

            return true;
        }
    }
}