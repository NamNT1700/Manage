using System;

namespace Manage.Model.DTO.Ward
{
    public class UpdateWardDTO
    {
        public int Id { get; set; }
        public UpdateWard updateData { get; set; }
    }
    public class UpdateWard
    {
        public string Name { get; set; }
        public int? DistricId { get; set; }
        
    }
}
