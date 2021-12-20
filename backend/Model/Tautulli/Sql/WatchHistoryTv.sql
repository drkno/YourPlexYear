select
    max(coalesce(nullif(h.grandparent_rating_key,''), 0)) as id,
    grandparent_title as title,
    sum(stopped - started) * 1000 as duration,
    min(sum(coalesce(nullif(view_offset,''),1.0))*100/sum(coalesce(nullif(duration,''),1.0)),100) as finishedPercent,
    count(*) as plays,
    count(distinct guid) as episodes,
    min(coalesce(nullif(year,''),0)) as year,
    thumb as thumbnail,
    sum(paused_counter) * 1000 as pausedDuration
from
    session_history h
    join session_history_metadata m
    on h.id = m.id
where
    m.media_type = 'episode'
    and user_id={0}
    and started >= {1}
    and stopped <= {2}
group by
    grandparent_title
order by
    duration
    DESC
