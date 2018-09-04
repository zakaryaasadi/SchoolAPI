using SchoolAPI.Models.MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolAPI.Models
{
    public class GroupClass
    {
        public int id { get; set; }
        public string name { get; set; }

        public GroupClass(GROUPS group)
        {
            if (group == null)
                return;

            id = group.GROUP_ID;
            name = group.NAME;
        }

        public GroupClass(int? groupId)
        {
            if (groupId == null)
                return;

            Entities e = new Entities();
            GROUPS group = e.GROUPS.FirstOrDefault(g => g.GROUP_ID == groupId);
            id = group.GROUP_ID;
            name = group.NAME;
        }
    }
}