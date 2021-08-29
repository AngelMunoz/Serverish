namespace Serverish.Views

open Giraffe.ViewEngine
open Serverish.BaseViews

[<RequireQualifiedAccess>]
module Home =

    let Index () =
        let content =
            [ Partials.Navbar()
              article [ _class "page" ] [
                  header [] [
                      h1 [] [ str "Welcome to Saturn!" ]
                  ]
                  p [] [
                      str
                          """
                          Saturn is an F# web framework for asp.net
                          """
                  ]

                  button [ attr "hx-post" "/dynamic-content"
                           attr "hx-target" "#content" ] [
                      str "Click to request content"
                  ]
                  section [ _id "content" ] []
              ] ]

        Layout.Default(content, "Home")


    let DynamicContent () =
        // I'm using raw here because there are custom-elements in the content
        // however you can deliver HTML as usual and then just use javascript
        // to do anykind of javascript code you can think of
        rawText
            """
             <article>
              <h1>Welcome to @curvenote/article</h1>

              <p>Let's create a simple example:</p>

              <r-scope name="simple">
                <r-var name="cookies" value="3" format=".4"></r-var>
                <p>
                  When you eat <r-dynamic bind="cookies" min="2" max="100" after=" cookies"></r-dynamic>,
                  you consume <r-display :value="cookies * 50" format=".0f">150</r-display> calories.
                </p>
              </r-scope>

              <hr>

              <h2>Charts</h2>

              <r-scope name="chart">
                <r-var name="x" value="20"></r-var>
                <r-var name="y" value="40"></r-var>
                $x$: <r-range bind="x"></r-range> <br>
                $y$: <r-range bind="y"></r-range>
                <r-svg-chart xlim="[0, 100]" ylim="[0, 100]">
                  <r-svg-path :data="[[0,0],[x,y]]"></r-svg-path>
                  <r-svg-node :x="x" :y="y" :drag="{x, y}"></r-svg-node>
                </r-svg-chart>

                <r-visible :visible="x > 50">
                  <aside class="callout danger">
                    <p>This is a important! <r-button dense :click="alert(`The values of x is LARGE: ${x}, ${y}`)" format=".0f"></r-button></p>
                  </aside>
                </r-visible>
              </r-scope>

              <hr>
              <aside class="margin">You can write in the margins!!</aside>
            </article>
            """
