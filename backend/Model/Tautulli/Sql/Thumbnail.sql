select
    coalesce(nullif(grandparent_thumb,''), thumb) as thumbnail
from
    session_history_metadata
where
    rating_key = {0}
    or grandparent_rating_key = '{0}'
order by
    id
    DESC
limit
    1
