
namespace SBFramework.Base.IO
{
    public static class FileExtensions
    {
        /// <see cref="WriteToFile(string, string, bool, bool, bool)"/>
        public static void WriteToFile(this string @this, string fileName)
        {
            FileActions.WriteFile(fileName, @this);
        }

        /// <see cref="WriteToFile(string, string, bool, bool, bool)"/>
        public static void WriteToFile(this string @this, string fileName, bool line)
        {
            FileActions.WriteFile(fileName, @this, line);
        }

        /// <summary>
        /// Escreve uma string no arquivo.
        /// </summary>
        /// <see cref="FileActions.WriteFile(string, object, bool, bool, bool)"/>
        public static void WriteToFile(this string @this, string fileName, bool line, bool append, bool encodingDefault)
        {
            FileActions.WriteFile(fileName, @this, line, append, encodingDefault);
        }

        /// <summary>
        /// Adiciona uma string ao final de um arquivo.
        /// </summary>
        /// <see cref="FileActions.WriteFile(string, object, bool, bool, bool)"/>
        public static void AppendToFile(this string @this, string fileName, bool encodingDefault)
        {
            FileActions.WriteFile(fileName, @this, true, true, encodingDefault);
        }

        /// <see cref="AppendToFile(string, string, bool)"/>
        public static void AppendToFile(this string @this, string fileName)
        {
            AppendToFile(@this, fileName, true);
        }
    }
}
