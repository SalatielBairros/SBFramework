using System;
using System.Collections.Generic;
using System.Linq;

namespace SBFramework.Base.List
{
    public static class ListExtensions
    {
        /// <summary>
        /// Executa uma ação para cada elemento de um array de strings.
        /// </summary>
        /// <param name="this">Array a ser manipulado.</param>
        /// <param name="act">Ação a ser executada em cada elemento.</param>
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> act)
        {
            foreach (var item in @this)
            {
                act(item);
            }
        }

        /// <summary>
        /// Busca número definido de ítens de uma lista de acordo com a condição.
        /// </summary>
        /// <param name="this">Lista a buscar os itens.</param>
        /// <param name="size">Número de itens a serem pegos.</param>
        /// <param name="map">Função condicional.</param>
        /// <returns>Itens retornados de acordo com a condição executada.</returns>
        public static IEnumerable<string> TakeWhen(this IEnumerable<string> @this, int size, Func<bool> map)
        {
            return map() ? @this.Take(size) : @this;
        }
    }
}
