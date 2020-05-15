import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { ConnectedNavMenu }  from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
        <div>
            <ConnectedNavMenu />
            <Container fluid className="m-0 p-0">
                {this.props.children}
            </Container>
        </div>
    );
  }
}
