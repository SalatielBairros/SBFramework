using System;
using System.IO;
using System.Linq;
using System.Text;
using SBFramework.Base.Functions;

namespace SBFramework.Base.IO
{
    public class FileActions
    {
        /// <summary>
        /// Realiza a leitura de um arquivo de texto.
        /// </summary>
        /// <param name="fileName">Nome do arquivo a ser lido.</param>
        /// <param name="encodingDefault">Informa se deve ser utilizado o encodingDefault (ANSI). Este encoding é utilizado quando a leitura é dos arquivos do FM.</param>
        /// <returns>Texto encontrado no arquivo.</returns>
        public static string ReadFile(string fileName, bool encodingDefault = false)
        {
            if (File.Exists(fileName))
            {
                return UsingFunctions.Using(
                    () => (encodingDefault ? new StreamReader(fileName, Encoding.Default, true) : File.OpenText(fileName)),
                    file => file.ReadToEnd().Trim());
            }
            return string.Empty;
        }

        /// <summary>
        /// Realiza a leitura de um arquivo de texto retornando as linhas de acordo com uma condição.
        /// </summary>
        /// <param name="fileName">Nome do arquivo a ser lido.</param>
        /// <param name="predicate">Condição para o retorno da linha.</param>
        /// <returns>Array com as linhas encontradas baseadas na condição.</returns>
        public static string[] ReadFileLinesWhen(string fileName, Func<string, bool> predicate)
        {
            return UsingFunctions.Using(
                () => File.OpenText(fileName),
                file => Enumerable.Where(file.ReadToEnd().Replace("\r",string.Empty).Split('\n'), predicate)).ToArray();
        }

        public static string[] ReadFileLines(string fileName)
        {
            return ReadFileLinesWhen(fileName, x => true);
        }

        /// <summary>
        /// Realiza a escrita em um arquivo de texto.
        /// </summary>
        /// <param name="fileName">Nome do arquivo.</param>
        /// <param name="content">Conteúdo que será escrito</param>
        /// <param name="line">Informa se deve ser adicionada uma nova linha.</param>
        /// <param name="append">Informa se o conteúdo deve ser adicionado ao final do arquivo ou sobreescrever o conteúdo existente.</param>
        /// <param name="encodingDefault">Informa se deve ser utilizado o encoding default (ANSI).</param>
        public static void WriteFile(string fileName, object content, bool line = false, bool append = false, bool encodingDefault = false)
        {
            UsingFunctions.UsingAct(
                () => (encodingDefault ? (new StreamWriter(fileName, append, Encoding.Default)) : (new StreamWriter(fileName, append))),
                file => file.Write(string.Concat(content.ToString(), (line ? Environment.NewLine : string.Empty))));
        }
    }
}
