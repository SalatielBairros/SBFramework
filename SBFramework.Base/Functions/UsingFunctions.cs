using System;

namespace SBFramework.Base.Functions
{
    public class UsingFunctions
    {
        /// <summary>
        /// Utilizado para todos os objetos que herdem da classe IDisposable e deve ser encerrados após utilização.
        /// Esta função executa a ação com essa classe dentro de uma cláusula Using de forma genérica.
        /// </summary>
        /// <typeparam name="TDisposable">Objeto a ser manipulado.</typeparam>
        /// <typeparam name="TResult">Resultado da manipulação</typeparam>
        /// <param name="factory">Criação da instância do objeto a ser manipulado.</param>
        /// <param name="map">Ação a ser executada com o objeto.</param>
        /// <returns>Retorno informado no parâmetro TResult.</returns>
        public static TResult Using<TDisposable, TResult>
            (Func<TDisposable> factory, Func<TDisposable, TResult> map)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                return map(disposable);
            }
        }

        /// <summary>
        /// Realiza a mesma ação do método Using, porém sem retorno nenhum.
        /// </summary>
        /// <typeparam name="TDisposable">Objeto a ser manipulado.</typeparam>        
        /// <param name="factory">Criação da instância do objeto a ser manipulado.</param>        
        /// <param name="act">Ação a ser executada com o objeto.</param>
        public static void UsingAct<TDisposable>
            (Func<TDisposable> factory, Action<TDisposable> act)
            where TDisposable : IDisposable
        {
            using (var disposable = factory())
            {
                act(disposable);
            }
        }
    }
}
