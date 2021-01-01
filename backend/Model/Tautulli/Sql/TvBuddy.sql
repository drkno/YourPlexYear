select
    count(*) as plays,
    user as buddy
from
    session_history h
    join session_history_metadata m
    on h.id = m.id
where
    m.media_type = 'episode'
    and grandparent_title = '{0}'
    and user_id!='{1}'
    and started >= 1577797200
    and stopped <= 1609419540
group by
    user_id
order by
    plays
    DESC
limit
    1
