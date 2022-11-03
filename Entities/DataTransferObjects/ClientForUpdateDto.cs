using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class ClientForUpdateDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public IEnumerable<OrderForCreationDto> Orders { get; set; }

    }
}