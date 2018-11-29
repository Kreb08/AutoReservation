using System;

namespace AutoReservation.BusinessLayer.Exceptions {
    public class InvalidDateException<T>
        : Exception {
        public InvalidDateException(string message) : base(message) { }
        public InvalidDateException(string message, T mergedEntity) : base(message) {
            MergedEntity = mergedEntity;
        }

        public T MergedEntity { get; set; }
    }
}
