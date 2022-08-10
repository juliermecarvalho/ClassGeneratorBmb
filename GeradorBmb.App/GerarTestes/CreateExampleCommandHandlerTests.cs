using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorBmb.App.GerarTestes
{
    public class CreateExampleCommandHandlerTests
    {
        private DirectoryInfo _directory;
        private string _nameClass;
        private string _nameUsing;
        const string abre = "{";
        const string fecha = "}";
        const string aspas = "\"";
        private string _usingTestes = "";

        public CreateExampleCommandHandlerTests(DirectoryInfo directoryInfo, string nameClass, string nameUsing)
        {
            _directory = directoryInfo;
            _nameClass = nameClass;
            _nameUsing = nameUsing;

            _usingTestes = @$"

using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Commands.Update.v1;
using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Contracts.Repositories.v1;
using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Commands.Create.v1;
using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Commands.Delete.v1;
using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Queries.GetAll.v1;
using Bmb.Funding.Operation.MasterData.Domain.{_nameClass}.Queries.GetOne.v1;
".Trim();
        }

        internal void Gerar()
        {
    
            DirectoryInfo directory = new(@$"{_directory.FullName}\{_nameClass}\Commands\Create\v1");
            if (!directory.Exists)
            {
                directory.Create();
            }

            CreateCommandHandlerTests(directory);
            CreateCommandValidadorlerTests(directory);

            DirectoryInfo directoryDelete = new(@$"{_directory.FullName}\{_nameClass}\Commands\Delete\v1");
            if (!directoryDelete.Exists)
            {
                directoryDelete.Create();
            }
            DeleteCommandHandlerTests(directoryDelete);
            DeleteCommandValidadorlerTests(directoryDelete);



            DirectoryInfo directoryUpdate = new(@$"{_directory.FullName}\{_nameClass}\Commands\Update\v1");
            if (!directoryUpdate.Exists)
            {
                directoryUpdate.Create();
            }
            UpdateCommandHandlerTests(directoryUpdate);
            UpdateCommandValidadorlerTests(directoryUpdate);



            DirectoryInfo directoryGetAll = new(@$"{_directory.FullName}\{_nameClass}\Queries\GetAll{_nameClass}Query\v1");
            if (!directoryGetAll.Exists)
            {
                directoryGetAll.Create();
            }
            GetAllCommandHandlerTests(directoryGetAll);
            


            DirectoryInfo directoryGetOne = new(@$"{_directory.FullName}\{_nameClass}\Queries\GetOne{_nameClass}Query\v1");
            if (!directoryGetOne.Exists)
            {
                directoryGetOne.Create();
            }

            GetOneCommandHandlerTests(directoryGetOne);
            GetOneCommandValidadorlerTests(directoryGetOne);

        }

        private void GetOneCommandValidadorlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\GetOne{_nameClass}QueryValidatorTests.cs");
            string linhas = @$"
{_usingTestes}
using Xunit;

namespace {_nameUsing}.{_nameClass}.Queries.GetOne{_nameClass}Query.v1;

public class GetOne{_nameClass}QueryValidatorTests
{abre}
    private readonly GetOne{_nameClass}QueryValidator _subject;

    public GetOne{_nameClass}QueryValidatorTests()
    {abre}
        _subject = new GetOne{_nameClass}QueryValidator();
    {fecha}
    
    [Fact(DisplayName = {aspas}GetOne{_nameClass}QueryValidator throw invalid query{aspas})]
    public void Should_indicate_invalid_query()
    {abre}
        var invalidQuery = new Domain.{_nameClass}.Queries.GetOne.v1.GetOne{_nameClass}Query(0);
        var result = _subject.Validate(invalidQuery);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    {fecha}

    [Fact(DisplayName = {aspas}GetOne{_nameClass}QueryValidator validate query successfully{aspas})]
    public void Should_validate_query_successfully()
    {abre}
        var validCommand = new Domain.{_nameClass}.Queries.GetOne.v1.GetOne{_nameClass}Query(1);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void GetOneCommandHandlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\GetOne{_nameClass}QueryHandlerTests.cs");
            string linhas = @$"
{_usingTestes}
using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Moq;
using Xunit;

namespace {_nameUsing}.{_nameClass}.Queries.GetOne{_nameClass}Query.v1;

public class GetOne{_nameClass}QueryHandlerTests
{abre}
    private const int {_nameClass}Id = 10;
    private readonly GetOne{_nameClass}QueryHandler _subject;
    private readonly Domain.{_nameClass}.Queries.GetOne.v1.GetOne{_nameClass}Query _query;
    private readonly Domain.{_nameClass}.Entities.v1.{_nameClass} _{_nameClass.ToLower()};

    private readonly INotificationContext _notificationContext;
    private readonly Mock<I{_nameClass}Repository> _{_nameClass.ToLower()}RepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public GetOne{_nameClass}QueryHandlerTests()
    {abre}
        _query = new Domain.{_nameClass}.Queries.GetOne.v1.GetOne{_nameClass}Query({_nameClass}Id);
        _{_nameClass.ToLower()} = new();

        _notificationContext = new NotificationContext();
        
        _{_nameClass.ToLower()}RepositoryMock = new Mock<I{_nameClass}Repository>();
        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_{_nameClass.ToLower()});
        
        _mapperMock = new Mock<IMapper>();

        _subject = new GetOne{_nameClass}QueryHandler(_notificationContext, _mapperMock.Object,
            _{_nameClass.ToLower()}RepositoryMock.Object);
    {fecha}

    [Fact(DisplayName = {aspas}Should set NotificationContext with NotFound type when not found entity{aspas})]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {abre}
        const int notExisting{_nameClass}Id = 20;

        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync(notExisting{_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.{_nameClass}.Entities.v1.{_nameClass}?)null);

        var result = await _subject.Handle(new Domain.{_nameClass}.Queries.GetOne.v1.GetOne{_nameClass}Query(notExisting{_nameClass}Id), CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    {fecha}

    [Fact(DisplayName = {aspas}Should map from entity to query result{aspas})]
    public async Task Should_map_from_entity_to_query_result()
    {abre}
        await _subject.Handle(_query, CancellationToken.None);
        
        _mapperMock.Verify(x=>x.Map<GetOne{_nameClass}QueryResult>(_{_nameClass.ToLower()}), Times.Once);
    {fecha}
    
    [Fact(DisplayName = {aspas}Should perform repository call to get {_nameClass}{aspas})]
    public async Task Should_perform_repository_call()
    {abre}
        await _subject.Handle(_query, CancellationToken.None);
        
        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void GetAllCommandHandlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\GetAll{_nameClass}QueryHandlerTests.cs");
            string linhas = @$"
{_usingTestes}
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Moq;
using Xunit;

namespace {_nameUsing}.{_nameClass}.Queries.GetAll{_nameClass}Query.v1;

public class GetAll{_nameClass}QueryHandlerTests
{abre}
    private readonly GetAll{_nameClass}QueryHandler _subject;
    private readonly IList<Domain.{_nameClass}.Entities.v1.{_nameClass}> _{_nameClass.ToLower()}s;
    private readonly Domain.{_nameClass}.Queries.GetAll.v1.GetAll{_nameClass}Query _query;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<I{_nameClass}Repository> _{_nameClass.ToLower()}RepositoryMock;
    

    public GetAll{_nameClass}QueryHandlerTests()
    {abre}
        _query = new ( true);

        _{_nameClass.ToLower()}s = new List<Domain.{_nameClass}.Entities.v1.{_nameClass}>();
            
        _{_nameClass.ToLower()}RepositoryMock = new Mock<I{_nameClass}Repository>();
        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetAll(_query, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_{_nameClass.ToLower()}s);

        _mapperMock = new Mock<IMapper>();
        
        _subject = new GetAll{_nameClass}QueryHandler(Mock.Of<INotificationContext>(), _mapperMock.Object, 
            _{_nameClass.ToLower()}RepositoryMock.Object);
    {fecha}

    [Fact(DisplayName = {aspas}Should perform repository call to get all {_nameClass}s{aspas})]
    public async Task Should_perform_repository_call()
    {abre}
        await _subject.Handle(_query, CancellationToken.None);
        
        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.GetAll(_query, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}

    [Fact(DisplayName = {aspas}Should map from database entity to query result{aspas})]
    public async Task Should_map_from_database_entity_to_query_result()
    {abre}
        await _subject.Handle(_query, CancellationToken.None);

        _mapperMock.Verify(x => x.Map<IList<GetAll{_nameClass}QueryResult>>(_{_nameClass.ToLower()}s), Times.Once);
    {fecha}
{fecha}
";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void UpdateCommandHandlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Update{_nameClass}CommandHandlerTests.cs");
            string linhas = @$"
{_usingTestes}
using AutoMapper;
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Moq;
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Update.v1;

public class Update{_nameClass}CommandHandlerTests
{abre}
    private const int {_nameClass}Id = 1;
    private readonly Mock<IMapper> _mapperMock;

    private readonly Domain.{_nameClass}.Commands.Update.v1.Update{_nameClass}Command _command;
    private readonly Domain.{_nameClass}.Entities.v1.{_nameClass} _{_nameClass.ToLower()};
    private readonly Update{_nameClass}CommandHandler _subject;
    private readonly INotificationContext _notificationContext;
    private readonly Mock<I{_nameClass}Repository> _{_nameClass.ToLower()}RepositoryMock;

    public Update{_nameClass}CommandHandlerTests()
    {abre}
        _command = new()
        {abre}
            Id = 1,
            Name = {aspas}Create TaxType Command{aspas},
            IsActive = true
        { fecha};

        _{_nameClass.ToLower()} = new ();
        _mapperMock = new Mock<IMapper>();
        _notificationContext = new NotificationContext();
        _{_nameClass.ToLower()}RepositoryMock = new Mock<I{_nameClass}Repository>();
        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_{_nameClass.ToLower()});
        
        _subject = new Update{_nameClass}CommandHandler(_notificationContext, _mapperMock.Object,_{_nameClass.ToLower()}RepositoryMock.Object);
    {fecha}
    
    [Fact(DisplayName = {aspas}Should set NotificationContext with NotFound type when not found entity{aspas})]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {abre}
        const int notExisting{_nameClass}Id = 20;

        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync(notExisting{_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.{_nameClass}.Entities.v1.{_nameClass}?)null);

        var result = await _subject.Handle(
            new Domain.{_nameClass}.Commands.Update.v1.Update{_nameClass}Command
            {abre}
                Id = 10,
                Name = {aspas}Create TaxType Command{aspas},
                IsActive = true
            {fecha}, CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    {fecha}

    [Fact(DisplayName = {aspas}Should perform repository call to get {_nameClass}{aspas})]
    public async Task Should_perform_repository_call()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);

        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}

    [Fact(DisplayName = {aspas}Should perform repository call to update {_nameClass}{aspas})]
    public async Task Should_perform_repository_call_to_update()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);

        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.UpdateAsync(_{_nameClass.ToLower()}, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void UpdateCommandValidadorlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Update{_nameClass}CommandValidadorTests.cs");
            string linhas = @$"
{_usingTestes}
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Update.v1;

public class Update{_nameClass}CommandValidatorTests
{abre}
    private readonly Update{_nameClass}CommandValidator _subject;

    public Update{_nameClass}CommandValidatorTests()
    {abre}
        _subject = new Update{_nameClass}CommandValidator();
    {fecha}
    
    [Fact(DisplayName = {aspas}Update{_nameClass}CommandValidator throw invalid command{aspas})]
    public void Should_indicate_invalid_command()
    {abre}
        var invalidCommand = new Domain.{_nameClass}.Commands.Update.v1.Update{_nameClass}Command
        {abre}
           
        {fecha};
        
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    {fecha}

    [Fact(DisplayName = {aspas}Update{_nameClass}CommandValidator validate command successfully{aspas})]
    public void Should_validate_command_successfully()
    {abre}
        var validCommand = new Domain.{_nameClass}.Commands.Update.v1.Update{_nameClass}Command
        {abre}
            Id = 1,
            Name = {aspas}Create TaxType Command{aspas},
            IsActive = true
        {fecha};
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void DeleteCommandHandlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Delete{_nameClass}CommandHandlerTests.cs");
            string linhas = @$"
{_usingTestes}
using Bmb.Core.Domain;
using Bmb.Core.Domain.Contracts;
using Bmb.Core.Domain.Enums;
using Moq;
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Delete.v1;

public class Delete{_nameClass}CommandHandlerTests
{abre}
    private const int {_nameClass}Id = 10;
    
    private readonly Delete{_nameClass}CommandHandler _subject;
    private readonly Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}Command _command;
    private readonly Domain.{_nameClass}.Entities.v1.{_nameClass} _{_nameClass.ToLower()};

    private readonly INotificationContext _notificationContext;
    private readonly Mock<I{_nameClass}Repository> _{_nameClass.ToLower()}RepositoryMock;

    public Delete{_nameClass}CommandHandlerTests()
    {abre}
        _command = new Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}Command({_nameClass}Id);
        _{_nameClass.ToLower()} = new Domain.{_nameClass}.Entities.v1.{_nameClass}(); /*verificar os contrutores.*/

        _notificationContext = new NotificationContext();
        
        _{_nameClass.ToLower()}RepositoryMock = new Mock<I{_nameClass}Repository>();
        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(_{_nameClass.ToLower()});

        _subject = new Delete{_nameClass}CommandHandler(_notificationContext,
            _{_nameClass.ToLower()}RepositoryMock.Object);
    {fecha}

    [Fact(DisplayName = {aspas}Should set NotificationContext with NotFound type when not found entity{aspas})]
    public async Task Should_set_NotificationContext_with_NotFound_notification_when_not_found_entity()
    {abre}
        const int notExisting{_nameClass}Id = 20;

        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.GetByIdAsync(notExisting{_nameClass}Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.{_nameClass}.Entities.v1.{_nameClass} ?)null);

        var result = await _subject.Handle(
            new Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}Command(notExisting{_nameClass}Id),
            CancellationToken.None);

        Assert.Null(result);
        Assert.NotEmpty(_notificationContext.Notifications);
        Assert.Equal(NotificationType.NotFound, _notificationContext.Type);
    {fecha}

    [Fact(DisplayName = {aspas}Should perform repository call to get {_nameClass}{aspas})]
    public async Task Should_perform_repository_call()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);
        
        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.GetByIdAsync({_nameClass}Id, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}
    
    [Fact(DisplayName = {aspas}Should perform repository call to inactivate {_nameClass}{aspas})]
    public async Task Should_perform_repository_call_to_inactivate()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);
        
        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.RemoveAsync(_{_nameClass.ToLower()}, It.IsAny<CancellationToken>()), Times.Once);
    {fecha}
{fecha}

";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void DeleteCommandValidadorlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Delete{_nameClass}CommandValidadorTests.cs");
            string linhas = @$"
{_usingTestes}
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Delete.v1;

public class Delete{_nameClass}CommandValidatorTests
{abre}
    private readonly Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}CommandValidator _subject;

    public Delete{_nameClass}CommandValidatorTests()
    {abre}
        _subject = new ();
    {fecha}
    
    [Fact(DisplayName = {aspas}Delete{_nameClass}CommandValidator throw invalid command{aspas})]
    public void Should_indicate_invalid_command()
    {abre}
        var invalidCommand = new Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}Command(0);
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    {fecha}

    [Fact(DisplayName = {aspas}Delete{_nameClass}CommandValidator validate command successfully{aspas})]
    public void Should_validate_command_successfully()
    {abre}
        var validCommand = new Domain.{_nameClass}.Commands.Delete.v1.Delete{_nameClass}Command(10);
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void CreateCommandValidadorlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Create{_nameClass}CommandHandlerTests.cs");
            string linhas = @$"
{_usingTestes}
using AutoMapper;
using Bmb.Core.Domain.Contracts;
using Moq;
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Create.v1;

public class Create{_nameClass}CommandHandlerTests
{abre}
    private const int {_nameClass}Id = 10;
    private readonly Create{_nameClass}CommandHandler _subject;
    private readonly Create{_nameClass}Command _command;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<I{_nameClass}Repository> _{_nameClass.ToLower()}RepositoryMock;

    public Create{_nameClass}CommandHandlerTests()
    {abre}
        _command = new Create{_nameClass}Command
        {abre}
           
        {fecha};
        
        _{_nameClass.ToLower()}RepositoryMock = new Mock<I{_nameClass}Repository>();
        _mapperMock = new Mock<IMapper>();

        _mapperMock.Setup(x =>
                x.Map<Domain.{_nameClass}.Entities.v1.{_nameClass}>(It.IsAny<Create{_nameClass}Command>()))
            .Returns(new Domain.{_nameClass}.Entities.v1.{_nameClass} {abre} {fecha});

        _{_nameClass.ToLower()}RepositoryMock.Setup(x => x.AddAsync(It.IsAny<Domain.{_nameClass}.Entities.v1.{_nameClass}>(), 
                It.IsAny<CancellationToken>())).Returns(Task.FromResult({_nameClass}Id));
        
        _subject = new Create{_nameClass}CommandHandler(Mock.Of<INotificationContext>(),
            _mapperMock.Object, _{_nameClass.ToLower()}RepositoryMock.Object);
    {fecha}

    [Fact(DisplayName = {aspas}Should map from command to entity{aspas})]
    public async Task Should_map_from_command_to_entity()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);
        
        _mapperMock.Verify(x => x.Map<Domain.{_nameClass}.Entities.v1.{_nameClass}>(_command), Times.Once);
    {fecha}

    [Fact(DisplayName = {aspas}Should add entity into repository{aspas})]
    public async Task Should_add_entity_into_repository()
    {abre}
        await _subject.Handle(_command, CancellationToken.None);
        
        _{_nameClass.ToLower()}RepositoryMock.Verify(x => x.AddAsync(It.IsAny<Domain.{_nameClass}.Entities.v1.{_nameClass}>(), 
            It.IsAny<CancellationToken>()), Times.Once);
    {fecha}
{fecha}


";


            file.WriteLine(linhas.Trim());
            file.Close();
        }

        private void CreateCommandHandlerTests(DirectoryInfo directory)
        {
            StreamWriter file = new(@$"{directory.FullName}\Create{_nameClass}CommandValidatorTests.cs");
            string linhas = @$"

{_usingTestes}
using Xunit;

namespace {_nameUsing}.{_nameClass}.Commands.Create.v1;

public class Create{_nameClass}CommandValidatorTests
{abre}
   
    
    private readonly Create{_nameClass}CommandValidator _subject;

    public Create{_nameClass}CommandValidatorTests()
    {abre}
        _subject = new Create{_nameClass}CommandValidator();
    {fecha}

    [Fact(DisplayName = {aspas}Create{_nameClass}CommandValidator throw invalid command{aspas})]
    public void Should_indicate_invalid_command()
    {abre}
        var invalidCommand = new Create{_nameClass}Command();
        var result = _subject.Validate(invalidCommand);
        
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    {fecha}

    [Fact(DisplayName = {aspas}Create{_nameClass}CommandValidator validate command successfully{aspas})]
    public void Should_validate_command_successfully()
    {abre}
        var validCommand = new Create{_nameClass}Command
        {abre}
            Name = {aspas}Create TaxType Command{aspas},
            IsActive = true
        { fecha};
        
        var result = _subject.Validate(validCommand);
        
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    {fecha}
{fecha}

";


            file.WriteLine(linhas.Trim());
            file.Close();
        }
    }
}
