select cast (strftime('%w', started/1000) as integer) as num,
       case cast (strftime('%w', started/1000) as integer)
           when 0 then 'Sunday'
           when 1 then 'Monday'
           when 2 then 'Tuesday'
           when 3 then 'Wednesday'
           when 4 then 'Thursday'
           when 5 then 'Friday'
           else 'Saturday' end as day,
       count(*) as count
from session_history
where user_id={0}
  and started >= {1}
  and stopped <= {2}
group by num, day
order by num asc;