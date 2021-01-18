namespace Monitor.Business.Common
{
    public class QrLabelModel
    {
        private string _qrCodeBody;

        /// <summary>
        /// Information to be encoded
        /// </summary>
        public string Body
        {
            get
            {
                return string.IsNullOrWhiteSpace(_qrCodeBody) ? string.Empty : _qrCodeBody;
            }
            private set
            {
                _qrCodeBody = value;
            }
        }

        public int Size { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body">Information to be encoded</param>
        public QrLabelModel(string body)
        {
            Body = body;
        }

        /// <summary>
        /// Checks if model is valid.
        /// </summary>
        /// <returns>True if model is valid, false otherwise.</returns>
        public virtual bool IsValid => !string.IsNullOrWhiteSpace(_qrCodeBody);
    }
}
