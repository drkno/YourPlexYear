import React from 'react';

import LoadingPlaceholder from './components/loading-placeholder';

import dataFetcher from '../api/dataFetcher';

import IntroPage from './IntroPage';
import SectionTopPage from './SectionTopPage';
import Top10Page from './Top10Page';
import MovieStatsPage from './MovieStatsPage';
import TvStatsPage from './TvStatsPage';
import WatchDayPage from './WatchDayPage';
import MostPopularPage from './MostPopularPage';
import BrowserUsagePage from './BrowserUsagePage';
import ConclusionPage from './ConclusionPage';

import './styles/YourPlexYear.css';

const ensure = (obj, ...path) => {
    for (let p of path) {
        obj[p] = obj[p] || {};
        obj = obj[p];
    }
}

class RootContainer extends React.Component {
    shouldLoadData = true;

    constructor(...args) {
        super(...args);
        this.state = {
            loaded: false
        };
    }

    async componentDidMount() {
        this.shouldLoadData = true;
        let data;
        try {
            data = await dataFetcher.getData(this.props.year);
            ensure(data, 'tv', 'popular');
            ensure(data, 'movies', 'popular');
            data = Object.assign({
                loaded: true
            }, data);
        } catch(e) {
            console.error(e);
            data = {
                loadingText: 'Error :('
            };
        }
        if (this.shouldLoadData) {
            this.setState(data);
        }
    }

    componentWillUnmount() {
        this.shouldLoadData = false;
    }

    render() {
        if (!this.state || !this.state.loaded) {
            return (<LoadingPlaceholder>{this.state.loadingText || 'Loading'}</LoadingPlaceholder>);
        }

        return (
            <div>
                <IntroPage nextAnchor='movies-overview' year={this.props.year}>
                    {this.state.username}
                </IntroPage>

                {/* Movies */}
                <SectionTopPage anchor='movies-overview'
                                nextAnchor='movies-top-10'
                                mostWatched={this.state.movies.top10[0]}
                                mediaType='movie'
                                total={this.state.movies.total}
                                year={this.props.year}>
                    Let's look at some
                </SectionTopPage>
                <Top10Page anchor='movies-top-10'
                           nextAnchor='movies-stats'
                           items={this.state.movies.top10}
                           mediaType='movie'
                           includeYear={true}
                           year={this.props.year} />
                <MovieStatsPage anchor='movies-stats'
                                nextAnchor='tv-overview'
                                year={this.props.year}>
                    {this.state.movies}
                </MovieStatsPage>

                {/* TV Shows */}
                <SectionTopPage anchor='tv-overview'
                                nextAnchor='tv-top-10'
                                mostWatched={this.state.tv.top10[0]}
                                mediaType='TV show'
                                total={this.state.tv.total}
                                year={this.props.year}>
                    Now, let's have a look at some
                </SectionTopPage>
                <Top10Page anchor='tv-top-10'
                           nextAnchor='tv-stats'
                           items={this.state.tv.top10}
                           mediaType='TV show'
                           includeYear={false}
                           year={this.props.year} />
                <TvStatsPage anchor='tv-stats' nextAnchor='watch-days' year={this.props.year}>
                    {this.state.tv}
                </TvStatsPage>

                {/* Watch Days */}
                <WatchDayPage anchor='watch-days'
                              nextAnchor='most-popular'
                              days={this.state.watchDays}
                              year={this.props.year} />

                {/* General Stats */}
                <MostPopularPage anchor='most-popular'
                                 nextAnchor='browser'
                                 tv={this.state.tv.popular}
                                 movie={this.state.movies.popular} />
                <BrowserUsagePage anchor='browser'
                                  nextAnchor='conclusion'
                                  yourBrowsers={this.state.yourBrowsers}
                                  globalBrowsers={this.state.globalBrowsers} />

                <ConclusionPage anchor='conclusion' ombi={this.state.ombi} tautulli={this.state.tautulli} />
            </div>
        );
    }
}

export default RootContainer;
