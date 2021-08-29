module Serverish.Program

open Giraffe
open Saturn
open Saturn.Endpoint
open Serverish.Views

let browser =
    pipeline {
        plug acceptHtml
        plug putSecureBrowserHeaders
        plug fetchSession
        set_header "x-pipeline-type" "Browser"
    }

let defaultView =
    router {
        get "/" (htmlView (Home.Index()))
        get "/index.html" (redirectTo false "/")
        get "/default.html" (redirectTo false "/")
        post "/dynamic-content" (htmlView (Home.DynamicContent()))
    }

let browserRouter =
    router {
        pipe_through browser

        forward "" defaultView
    }

let appRouter = router { forward "" browserRouter }

[<EntryPoint>]
let main args =
    let app =
        application {
            use_developer_exceptions
            use_endpoint_router appRouter
            use_static "wwwroot"
        }

    run app
    0
