#load "imports.fsx"
#load "vk.fsx"

open VK

let nature = searchPhoto "кофе"

for photo in nature.Data.Items do
    printfn "%A" photo

