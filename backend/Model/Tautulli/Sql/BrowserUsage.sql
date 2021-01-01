select
    user_id,
    platform,
    count(*) as count
from
    session_history
where
    started >= 1577797200
    and stopped <= 1609419540
group by
    user_id,
    platform
order by
    count
    DESC;
