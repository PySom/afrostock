import React from "react";
import "./_ResultArea.scss";
import RecentSearch from "./RecentSearch";
//import TrendingArea from './TrendingArea';
import { Link, useHistory } from "react-router-dom";
import ls from "../../../sideEffects/local/ourLocalStorage";

export default function ResultArea({
  style,
  results,
  term,
  onMouseOver,
  onMouseOut,
}) {
  const recentSearch = ls.getItemInLs("recent");
  const history = useHistory();
  const onClick = (search) => {
    history.push("/contents/" + search);
  };
  const clearRecent = () => {
    ls.removeItemFromLS("recent");
  };
  return (
    <div
      className="w-100 a-p t-0"
      style={style}
      onMouseOver={onMouseOver}
      onMouseOut={onMouseOut}
    >
      {results &&
        results.length > 0 &&
        term &&
        results.map((data) => (
          <div key={data.id}>
            {console.log({ term })}
            {console.log({ name: data.name })}
            {console.log(data.name.substring(0, term.length))}
            {console.log(
              data.name
                .toLowerCase()
                .substring(data.name.indexOf(term.toLowerCase()) + term.length)
            )}
            <Link
              to={`/contents/${data.name}`}
              className="search-bar__suggestions"
            >
              <span className="typed__value">
                {data.name.substring(0, term.length)}
              </span>
              <span className="auto__complete" style={{ color: "#a6aaad" }}>
                {data.name.substring(
                  data.name.toLowerCase().indexOf(term.toLowerCase()) +
                    term.length
                )}
              </span>
            </Link>
          </div>
        ))}

      {recentSearch && (
        <div className="mt-4 mb-4">
          <h5 className="f-12 mb-3">
            Recent Searches
            <button onClick={clearRecent} className="unstyled">
              <span className="clear-search">x</span>
            </button>
          </h5>
          {recentSearch.map((search, index) => (
            <RecentSearch
              key={index}
              name={search}
              onClick={() => onClick(search)}
            />
          ))}
        </div>
      )}
      {/*
			{
				trending.length &&
				(
					<div className="mt-4 mb-4">
						<h5 className="f-12 mb-3">Trending Topics</h5>
						<div className="app-flex">
							{trending.map((trend) => (<TrendingArea key={trend.id} name={trend.name} src={trend.src} id={trend.id} />))}
						</div>
					</div>
				)
			}*/}
    </div>
  );
}
