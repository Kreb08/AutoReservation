using AutoReservation.BusinessLayer.Exceptions;
using AutoReservation.Dal;

namespace AutoReservation.BusinessLayer
{
    public abstract class ManagerBase
    {
        protected static OptimisticConcurrencyException<T> CreateOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            T dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new OptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }

        protected static InvalidDateException<T> CreateInvalidDateException<T>(AutoReservationContext context, T entity)
            where T : class {
            T dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new InvalidDateException<T>($"{typeof(T).Name}: Dauer zu kurz oder falsches Datum", dbEntity);
        }

        protected static AutoUnavailableException<T> CreateAutoUnavailableException<T>(AutoReservationContext context, T entity)
            where T : class {
            T dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new AutoUnavailableException<T>($"{typeof(T).Name}: Auto nicht verfügbar", dbEntity);
        }
    }
}