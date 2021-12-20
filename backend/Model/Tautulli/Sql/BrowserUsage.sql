select
    user_id,
    platform,
    count(*) as count
from
    session_history
where
    started >= {0}
    and stopped <= {1}
group by
    user_id,
    platform
order by
    count
    DESC;
