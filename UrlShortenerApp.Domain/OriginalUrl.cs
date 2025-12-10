using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortenerApp.Domain
{
    public class OriginalUrl
    {
        private OriginalUrl() { }

        public OriginalUrl(string shortCode, string originalLink, DateTime expirationDate)
        {
            if (string.IsNullOrWhiteSpace(shortCode))
            {
                throw new ArgumentException("Short code cannot be null or empty.", nameof(shortCode));
            }
            if (string.IsNullOrWhiteSpace(originalLink))
            {
                throw new ArgumentException("Original link cannot be null or empty.", nameof(originalLink));
            }
            if(expirationDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Expiration date must be later than the creation date.", nameof(expirationDate));
            }
            ShortCode = shortCode;
            OriginalLink = originalLink;
            CreatedOn = DateTime.UtcNow;
            ExpirationDate = expirationDate;
            ClickCount = 0;
            IsActive = true;
        }

        [Key]
        public string ShortCode { get; private set; }
        public string OriginalLink { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public int ClickCount { get; private set; }
        public bool IsActive { get; private set; }

        public void IncrementClickCount()
        {
            ClickCount++;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetExpirationDate(DateTime expirationDate)
        {
            if (expirationDate <= CreatedOn)
            {
                throw new ArgumentException("Expiration date must be later than the creation date.");
            }
            ExpirationDate = expirationDate;
        }

        public void UpdateOriginalLink(string newLink)
        {
            if(string.IsNullOrWhiteSpace(newLink))
            {
                throw new ArgumentException("Original link cannot be null or empty.", nameof(newLink));
            }
            OriginalLink = newLink;
        }
    }
}
