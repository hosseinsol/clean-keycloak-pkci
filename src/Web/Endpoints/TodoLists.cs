using clean_keycloak_pkce.Application.TodoLists.Commands.CreateTodoList;
using clean_keycloak_pkce.Application.TodoLists.Commands.DeleteTodoList;
using clean_keycloak_pkce.Application.TodoLists.Commands.UpdateTodoList;
using clean_keycloak_pkce.Application.TodoLists.Queries.GetTodos;

namespace clean_keycloak_pkce.Web.Endpoints;

public class TodoLists : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetTodoLists)
            .MapPost(CreateTodoList)
            .MapPut(UpdateTodoList, "{id}")
            .MapDelete(DeleteTodoList, "{id}");
    }

    public Task<TodosVm> GetTodoLists(ISender sender)
    {
        return  sender.Send(new GetTodosQuery());
    }

    public Task<int> CreateTodoList(ISender sender, CreateTodoListCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateTodoList(ISender sender, int id, UpdateTodoListCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteTodoList(ISender sender, int id)
    {
        await sender.Send(new DeleteTodoListCommand(id));
        return Results.NoContent();
    }
}
