using System;

namespace Manage.Model.DTO.Ward
{
    public class WardDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? DistricId { get; set; }
        public string Activeflg { get; set; }
    }
}
