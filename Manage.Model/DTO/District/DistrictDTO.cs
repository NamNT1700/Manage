using System;

namespace Manage.Model.DTO.District
{
    public class DistrictDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
    }
}
