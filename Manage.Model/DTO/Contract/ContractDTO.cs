using System;

namespace Manage.Model.DTO.Contract
{
    public class ContractDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public int? NumberOfMonth { get; set; }
        public double? Money { get; set; }
       
    }
}
