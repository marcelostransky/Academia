using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaDanca.IO.App.Helper
{
    public static class Criptografia
    {
        private static SymCryptography _objSenha;

        /// <summary>
        /// Gera uma senha criptografada com letras e número aleatórios
        /// </summary>
        /// <returns></returns>
        public static string GeraSenhaCriptografada()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Encrypt(builder.ToString());

        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <summary>
        /// Generates a random string with the given length
        /// </summary>
        /// <param name="size">Size of the string</param>
        /// <param name="lowerCase">If true, generate lowercase string</param>
        /// <returns>Random string</returns>
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(26 * random.NextDouble() + 65));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        /// <summary>
        /// Retorna a senha descriptofrada "Rijndael"
        /// </summary>
        /// <param name="Senha"></param>
        /// <returns></returns>
        public static string RetornaSenhaDescriptografada(string Senha)
        {
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Decrypt(Senha);
        }

        /// <summary>
        /// Criptografa a string e retorna.
        /// </summary>
        /// <param name="Senha"></param>
        /// <returns></returns>
        public static string RetornaSenhaCriptografada(string Senha)
        {
            _objSenha = new SymCryptography("Rijndael");
            return _objSenha.Encrypt(Senha);
        }

    }
}
