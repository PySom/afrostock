import React from 'react';
import './ResultArea.css';
//import RecentSearch from './RecentSearch';
//import TrendingArea from './TrendingArea';
import { Link } from 'react-router-dom';

//const recent = [
//	{
//		name: "Full moon"
//	},
//	{
//		name: "Business"
//    }
//]

//const trending = [
//	{
//		name: "Love",
//		src: "https://images.pexels.com/photos/3183150/pexels-photo-3183150.jpeg?auto=compress&crop=entropy&cs=tinysrgb&dpr=2&fit=crop&h=50&w=50",
//		id: 1
//	},
//	{
//		name: "News",
//		src: "https://images.pexels.com/photos/3183150/pexels-photo-3183150.jpeg?auto=compress&crop=entropy&cs=tinysrgb&dpr=2&fit=crop&h=50&w=50",
//		id: 2
//	},
//	{
//		name: "Women",
//		src: "https://images.pexels.com/photos/3183150/pexels-photo-3183150.jpeg?auto=compress&crop=entropy&cs=tinysrgb&dpr=2&fit=crop&h=50&w=50",
//		id: 3
//	}
//]



export default function ResultArea({ style, results, term, onMouseOver, onMouseOut, onClick }) {
	console.log({ results })
	return (
		<div className="w-100 a-p t-0" style={style} onMouseOver={onMouseOver} onMouseOut={onMouseOut}>
			{
				(results && results.length > 0 && term) &&
				results.map((data) => (
					<div key={data.id}>
						{console.log({term})}
						{console.log({ name: data.name })}
						{console.log(data.name.substring(0, term.length))}
						{console.log(data.name.toLowerCase().substring(data.name.indexOf(term.toLowerCase()) + term.length))}
						<Link to={`/videos/${data.name}`} className="search-bar__suggestions">
							<span>{data.name.substring(0, term.length)}</span>
							<span style={{ color: "#a6aaad" }}>{data.name.substring(data.name.toLowerCase().indexOf(term.toLowerCase()) + term.length)}</span>
						</Link>
					</div>
				))
            }

			{/*{
				recent.length &&
				(
					<div className="mt-4 mb-4">
						<h5 className="f-12 mb-3">Recent Searches</h5>
						{recent.map((search, index) => (<RecentSearch key={index} name={search.name} />))}
					</div>
				)
			}

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
	)
}
