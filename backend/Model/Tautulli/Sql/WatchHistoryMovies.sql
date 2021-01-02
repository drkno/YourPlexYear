select
    max(coalesce(nullif(h.rating_key,''), 0)) as id,
    title,
    sum(stopped - started) * 1000 as duration,
    min(max(coalesce(nullif(view_offset,''),1.0))*100/max(coalesce(nullif(duration,''),1.0)),100) as finishedPercent,
    count(*) as plays,
    min(coalesce(nullif(year,''),0)) as year,
    thumb as thumbnail,
    sum(paused_counter) * 1000 as pausedDuration
from
    session_history h
    join session_history_metadata m
    on h.id = m.id
where
    m.media_type = 'movie'
    and user_id={0}
    and started >= 1577797200
    and stopped <= 1609419540
group by
    title
order by
    duration
    DESC