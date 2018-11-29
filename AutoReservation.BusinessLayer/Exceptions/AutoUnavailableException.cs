using System;

namespace AutoReservation.BusinessLayer.Exceptions {
    public class AutoUnavailableException<T>
        : Exception {
        public AutoUnavailableException(string message) : base(message) { }
        public AutoUnavailableException(string message, T mergedEntity) : base(message) {
            MergedEntity = mergedEntity;
        }

        public T MergedEntity { get; set; }
    }
}
