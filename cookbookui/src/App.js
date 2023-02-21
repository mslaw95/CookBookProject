import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Pages from './pages/Pages';

function App() {
    return (
        <div className="App">
          <h1> Hello </h1>
          <Pages />
        </div>
    )
}
        /*<Router>
            <Routes>
                <Route path="/recipelist" element={<RecipeList/>}/>
                <Route path="/newrecipeform" element={<NewRecipeForm/>}/>
            </Routes>
        </Router>
    );*/

export default App;