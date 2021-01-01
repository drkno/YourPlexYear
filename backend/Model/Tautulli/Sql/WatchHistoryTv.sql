select grandparent_title as title, sum(stopped - started) as duration, count(*) as plays, min(year) as year, thumb as thumbnail from session_history h join session_history_metadata m on h.id=m.id where m.media_type = 'episode' and user_id={0} and started >= 1577797200 and stopped <= 1609419540 group by grandparent_title order by duration DESC