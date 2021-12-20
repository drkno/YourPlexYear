select id, title, plays, thumbnail from (
    select
        max(h.rating_key) as id,
        title,
        count(*) as plays,
        thumb as thumbnail
    from
        session_history h
        join session_history_metadata m
        on h.id = m.id
    where
        m.media_type = 'movie'
        and started >= {0}
        and stopped <= {1}
    group by
        title
    order by
        plays
        DESC
    limit
        1
)
union all
select id, title, plays, thumbnail from (
    select
        max(h.grandparent_rating_key) as id,
        grandparent_title as title,
        count(*) as plays,
        thumb as thumbnail
    from
        session_history h
        join session_history_metadata m
        on h.id = m.id
    where
        m.media_type = 'episode'
        and started >= {0}
        and stopped <= {1}
    group by
        grandparent_title
    order by
        plays
        DESC
    limit
        1
);
