import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import RecipeList from './pages/RecipeList';
import NewRecipeForm from './pages/NewRecipeForm';

class App extends React.Component {
  render() {
    return (
        <Router>
            <Routes>
                <Route path="/recipelist" element={<RecipeList/>}/>
                <Route path="/newrecipeform" element={<NewRecipeForm/>}/>
            </Routes>
        </Router>
    );
  }
}

export default App;