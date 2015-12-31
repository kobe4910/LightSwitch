using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuData
{
    public class TeacherReposity
    {
        public byte[] GetTeacherPhoto(string persNO)
        {
            using (var photo=new photosEntities())
            {
                var result = photo.tab_photos.FirstOrDefault(p => p.uid == persNO);
                if (result != null)
                    return result.ph;
            }
            return null;
        }

        public shu_teacher_all GetTeacherInfo(string persNo)
        {
            using (var conn=new shudatabasecenterEntities())
            {
                return conn.shu_teacher_all.FirstOrDefault(p => p.GH == persNo);
            }
        }
    }
}

