using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Models;

namespace Malshinon.Dal
{
    public static  class AlertDal
    {
        public static void AddAlert(Alert alert)
        {
            string sql = @"INSERT INTO alerts (target_id , reason ,window_start, window_end, alert_time)
                          VALUES (@target_id,@reason,@window_start,@window_end,@alert_time)";
            var parameters = new Dictionary<string, object>
            {
                {"@target_id",alert.TargetId },
                {"@reason",alert.Reason },
                {"@window_start",alert.WindowStart },
                {"@window_end" ,alert.WindowEnd},
                {"@alert_time" ,alert.AlertTime},

            };
            DBConnection1.ExecuteNonQuery(sql, parameters);
        }
    }
}
